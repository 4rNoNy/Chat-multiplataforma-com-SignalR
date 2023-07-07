import 'package:flutter/material.dart';

import '../models/imageOfPost.dart';

class DetailPhotoScreen extends StatelessWidget {
  final List<ImageOfPost> images;

  const DetailPhotoScreen({Key? key, required this.images}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      extendBodyBehindAppBar: true,
      backgroundColor: Colors.white.withOpacity(0.2),
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        elevation: 0,
      ),
      body: ListView.builder(
        itemCount: images.length,
        itemBuilder: (context, index) => Container(
          margin: const EdgeInsets.only(bottom: 8),
          child: AspectRatio(
            aspectRatio: 9 / 15, // Defina a proporção desejada (largura / altura)
            child: Padding(
              padding: const EdgeInsets.only(top: 100,bottom: 40), // Ajuste o valor do espaçamento superior conforme necessário
              child: Center(
                child: Container(
                  decoration: BoxDecoration(
                    image: DecorationImage(
                      image: NetworkImage(images[index].path),
                      fit: BoxFit.cover,
                    ),
                  ),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
