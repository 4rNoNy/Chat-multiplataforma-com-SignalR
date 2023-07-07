import { Col, Row, Card, Alert, Button } from "react-bootstrap";
import '../../css/Home.css';

export default function HomePage() {
    return (
        <div>
            <div className="hero-section">
                <div className="container-div">
                    <div className="flexbox flexbox-vertical">
                        <div className="left-block">
                            <h1 className="heading">Tenha o seu<br />melhor chat</h1>
                            <p className="paragraph">Bate-papo em equipe ilimitado, rápido &amp; fácil.</p>
                            <Button variant="primary" href="/login" className="button w-button">
                                Experimente!
                                </Button>
                            <Button variant="primary" href="/new-feed" className="ghost-button w-button">
                                Veja as Novidades
                            </Button>
                        </div>
                        <div className="right-block">
                            <img
                                src="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9e131121922fa4f55de3ef_hero-min.png"
                                width="585"
                                srcSet="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9e131121922fa4f55de3ef_hero-min-p-500.png 500w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9e131121922fa4f55de3ef_hero-min.png 1170w"
                                sizes="(max-width: 479px) 87vw, (max-width: 767px) 75vw, (max-width: 991px) 41vw, 50vw"
                                alt=""
                            />
                        </div>
                    </div>
                </div>
            </div>
            <div className="section">
                <div className="container center">
                    <h1>Bate-papos instantâneos em equipe</h1>
                    <p className="paragraph-2">
                        Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.
                        Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis,
                        ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa.
                    </p>
                    <img
                        src="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f38c1bc36677df6ebb58f_Group%2018-min.jpg"
                        width="1112"
                        srcSet="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f38c1bc36677df6ebb58f_Group%2018-min-p-800.jpeg 800w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f38c1bc36677df6ebb58f_Group%2018-min-p-1080.jpeg 1080w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f38c1bc36677df6ebb58f_Group%2018-min-p-1600.jpeg 1600w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f38c1bc36677df6ebb58f_Group%2018-min-p-2000.jpeg 2000w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f38c1bc36677df6ebb58f_Group%2018-min.jpg 2224w"
                        sizes="(max-width: 479px) 100vw, (max-width: 767px) 92vw, (max-width: 991px) 88vw, 90vw"
                        alt=""
                        className="image-2"
                    />
                </div>
            </div>
            <div className="section cta-section">
                <div className="container">
                    <div className="flexbox flexbox-vertical">
                        <div className="cta-left">
                            <img src="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f469cf6e21d905dc31f6a_Group%2020.png" width="816" srcSet="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f469cf6e21d905dc31f6a_Group%2020-p-500.png 500w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f469cf6e21d905dc31f6a_Group%2020-p-800.png 800w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f469cf6e21d905dc31f6a_Group%2020-p-1080.png 1080w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f469cf6e21d905dc31f6a_Group%2020-p-1600.png 1600w, https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f469cf6e21d905dc31f6a_Group%2020.png 1632w" sizes="(max-width: 767px) 100vw, (max-width: 991px) 65vw, 67vw" alt="" />
                        </div>
                        <div className="cta-right">
                            <h1>Solução perfeita para pequenas empresas</h1>
                            <p className="paragraph-3">Planos de preços que se encaixam como uma luva.</p>
                            <a href="/login" className="button blue-botton w-button">Experimente!</a>
                            <a href="/new-feed" className="ghost-button blue-ghost-button w-button">Veja as Novidades</a>
                            <div className="div-block"></div>
                            <div className="flexbox stars-flexbox">
                                <img src="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f4c71baf5bcff5bb2ec6a_Group%2021.svg" alt="" />
                                <div className="text-block">
                                    <strong>5,200 empresas </strong>usam o ChatApp e o classificam como<br /><strong>5 estrelas</strong>.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="section footer-section">
                <div className="container">
                    <div className="flexbox footer">
                        <div className="footer-column first-column">
                            <img src="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9f53678b12020c6a12858f_chatapp-blue.svg" alt="" />
                            <div className="text-block-2">O último bate-papo em equipe que você precisará.</div>
                        </div>
                        <div className="footer-column hide"></div>
                        <div className="footer-column">
                            <h3 className="heading-4">Ajuda</h3>
                            <a href="#" className="link">Suporte</a>
                            <a href="#" className="link">Base de conhecimento</a>
                            <a href="#" className="link">Tutorial</a>
                        </div>
                        <div className="footer-column">
                            <h3 className="heading-4">Características</h3>
                            <a href="#" className="link">Compartilhamento de tela</a>
                            <a href="#" className="link">Apps iOS &amp; Android</a>
                            <a href="#" className="link">Compartilhamento de arquivos</a>
                            <a href="#" className="link">Gerenciamento de usuários</a>
                        </div>
                        <div className="footer-column">
                            <h3 className="heading-4">Empresa</h3>
                            <a href="#" className="link">Sobre nós</a>
                            <a href="#" className="link">Carreiras</a>
                            <a href="#" className="link">Contate-nos</a>
                        </div>
                        <div className="footer-column last-column">
                            <h3 className="heading-4">Contate-nos</h3>
                            <a href="4rnony@gmail.com" className="link">4rnony@gmail.com</a>
                            <a href="tel:+5527992248102" className="link">(+55)27 992248102</a>
                            <a href="https://goo.gl/maps/NBLMTCW3E8FqMKdD7?coh=178572&entry=tt" target="_blank" className="link">Jardim Santa Rosa<br />Guarapari - ES</a>
                        </div>
                    </div>
                    <div className="text-block-3">© Copyright ChatApp Inc.
                    </div>
                </div>
            </div>
        </div>
    );
}
