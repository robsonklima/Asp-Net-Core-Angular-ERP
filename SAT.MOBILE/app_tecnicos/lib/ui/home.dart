import 'package:flutter/material.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.blue,
        title: const Text('Lista de Chamados'),
        automaticallyImplyLeading: false,
      ),
      body: ListView(
        children: <Widget>[
          ListTile(
            title: const Text('97229380'),
            onTap: () {},
          ),
          ListTile(
            title: const Text('97229312'),
            onTap: () {},
          ),
          ListTile(
            title: const Text('97223307'),
            onTap: () {},
          ),
        ],
      ),
    );
  }
}
