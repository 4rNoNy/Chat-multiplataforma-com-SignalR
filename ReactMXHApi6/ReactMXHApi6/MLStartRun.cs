using Microsoft.ML;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.MLDataStructures;

namespace ReactMXHApi6
{
    public enum MyTrainerStrategy : int { SdcaMultiClassTrainer = 1, OVAAveragedPerceptronTrainer = 2 };

    public class MLStartRun
    {
        public static string DataSetLocation = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data", "corefx-issues-train.tsv");
        public static string ModelPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MLModels", "PhanLoaiLabelerModel.zip");

        public static void BuildAndTrainModel(MyTrainerStrategy selectedStrategy)
        {
            // Criar MLContext para ser compartilhado entre os objetos de fluxo de trabalho de criação de modelo
            // Defina uma seed aleatória para resultados repetíveis/determinísticos em vários treinamentos.
            var mlContext = new MLContext(seed: 1);

            // PASSO 1: Configuração comum de carregamento de dados
            var trainingDataView = mlContext.Data.LoadFromTextFile<PostData>(DataSetLocation, hasHeader: true, separatorChar: '\t', allowSparse: false);

            // ETAPA 2: configuração de processo de dados comum com transformações de dados de pipeline
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: nameof(PostData.Category))
                            .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "ContentFeaturized", inputColumnName: nameof(PostData.NoiDung)))
                            .Append(mlContext.Transforms.Concatenate(outputColumnName: "Features", "ContentFeaturized"))
                            .AppendCacheCheckpoint(mlContext);


            // PASSO 3: Crie o algoritmo/trainer 
            IEstimator<ITransformer> trainer = null;
            switch (selectedStrategy)
            {
                case MyTrainerStrategy.SdcaMultiClassTrainer:
                    trainer = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features");
                    break;
                case MyTrainerStrategy.OVAAveragedPerceptronTrainer:
                    {
                        // Crie um trainer de classificação binária.
                        var averagedPerceptronBinaryTrainer = mlContext.BinaryClassification.Trainers.AveragedPerceptron("Label", "Features", numberOfIterations: 10);
                        // Componha um treinador OVA (One-Versus-All) com o BinaryTrainer.
                        // Nesta estratégia, um algoritmo de classificação binária é usado para treinar um classificador para cada classe, "
                        // que distingue essa classe de todas as outras classes. A previsão é então executada executando esses classificadores binários, "
                        // e escolhendo a previsão com a pontuação de confiança mais alta.
                        trainer = mlContext.MulticlassClassification.Trainers.OneVersusAll(averagedPerceptronBinaryTrainer);

                        break;
                    }
                default:
                    break;
            }

            //Define o trainer/algorithm e o rótulo do mapa para o valor (estado legível original)
            var trainingPipeline = dataProcessPipeline.Append(trainer)
                    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // PASSO 4: Validação cruzada com um único conjunto de dados (já que não temos dois conjuntos de dados, um para treinamento e outro para avaliação)
            // para avaliar e obter as métricas de precisão do modelo

            Console.WriteLine("=============== Validação cruzada para obter as métricas de precisão do modelo ===============");
            var crossValidationResults = mlContext.MulticlassClassification.CrossValidate(data: trainingDataView, estimator: trainingPipeline, numberOfFolds: 6, labelColumnName: "Label");

            // PASSO 5: Treine o ajuste do modelo ao DataSet
            Console.WriteLine("=============== Treinando o modelo ===============");
            var trainedModel = trainingPipeline.Fit(trainingDataView);

            var issue = new PostData() { Id = "Any-id", NoiDung = "Selling Phone" };
            // Cria mecanismo de previsão relacionado ao modelo treinado carregado
            var predEngine = mlContext.Model.CreatePredictionEngine<PostData, PostPrediction>(trainedModel);
            //Pontuação
            var prediction = predEngine.Predict(issue);
            Console.WriteLine($"=============== Modelo recém-treinado de previsão única - Resultado: {prediction.Area} ===============");
            //

            // ETAPA 6: Salve/persista o modelo treinado em um arquivo .ZIP
            Console.WriteLine("=============== Salvando o modelo em um arquivo ===============");
            mlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath);
        }

        public static void TestSingleLabelPrediction(PostData data)
        {
            var labeler = new Labeler(modelPath: ModelPath);
            labeler.TestPredictionForSingleIssue(data);
        }
    }
}
