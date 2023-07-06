import 'package:app_tecnicos/constants/constants.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(home: Home());
  }
}

class Home extends StatefulWidget {
  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  late Response response;
  Dio dio = Dio();

  bool error = false;
  bool loading = false;
  String errmsg = "";
  var apidata;

  @override
  void initState() {
    getData();
    super.initState();
  }

  getData() async {
    setState(() {
      loading = true;
    });

    final response = await dio.post('${Constants.API_URL}/Usuario/Login',
        data: {'codUsuario': 'ealmanca', 'senha': 'Eroa@608'});
    apidata = response.data;

    if (response.statusCode == 200) {
    } else {}

    loading = false;
    setState(() {}); //refresh UI
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Fetch Data from REST API"),
          backgroundColor: Colors.redAccent,
        ),
        body: Container(
            alignment: Alignment.topCenter,
            padding: EdgeInsets.all(20),
            child: loading
                ? CircularProgressIndicator()
                : //if loading == true, show progress indicator
                Container(
                    //if there is any error, show error message
                    child: error
                        ? Text("Error: $errmsg")
                        : Column(
                            //if everything fine, show the JSON as widget
                            children: apidata["data"].map<Widget>((country) {
                              return Card(
                                child: ListTile(
                                  title: Text(country["name"]),
                                  subtitle: Text(country["capital"]),
                                ),
                              );
                            }).toList(),
                          ))));
  }
}
