import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

import '../types/album.types.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({Key? key}) : super(key: key);

  Future<List<Album>> fetchAlbuns() async {
    final response = await http
        .get(Uri.parse('https://jsonplaceholder.typicode.com/albums'));

    if (response.statusCode == 200) {
      // final teste = Album.fromJson(jsonDecode(response.body));
      // final teste2 = 123;

      List jsonResponse = json.decode(response.body);
      final albuns = jsonResponse.map((data) => Album.fromJson(data)).toList();
      final teste = 12;
    } else {
      throw Exception('Failed to load album');
    }

    return [];
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.teal,
        title: const Text('Lista de Posts'),
      ),
      body: Column(
        children: [
          Expanded(
            child: FutureBuilder<List<Album>>(
              future: fetchAlbuns(),
              builder: (context, snapshot) {
                if (snapshot.hasError) {
                  return Center(
                    child: Text(
                      snapshot.error.toString(),
                    ),
                  );
                }

                if (!snapshot.hasData) {
                  return const Center(
                    child: CircularProgressIndicator(),
                  );
                }

                List<Album>? posts = snapshot.data;

                return posts != null && posts.isNotEmpty
                    ? ListView.builder(
                        itemCount: posts.length,
                        itemBuilder: (_, i) {
                          return Text(posts[i].title!);
                        },
                      )
                    : const Center(
                        child: Text('Ops, sem posts'),
                      );
              },
            ),
          ),
        ],
      ),
    );
  }
}
