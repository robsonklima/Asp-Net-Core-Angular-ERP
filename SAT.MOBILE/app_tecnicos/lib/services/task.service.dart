import 'dart:convert';
import 'dart:developer';
import 'dart:io';
import 'package:app_tecnicos/types/task.types.dart';
import 'package:http/http.dart' as http;

import '../constants/constants.dart';

Future<http.Response?> register(SatTask data) async {
  http.Response? response;
  try {
    response = await http.post(Uri.parse(Constants.API_URL + '/Task'),
        headers: {
          HttpHeaders.contentTypeHeader: "application/json",
        },
        body: jsonEncode(data.toJson()));
  } catch (e) {
    log(e.toString());
  }
  return response;
}
