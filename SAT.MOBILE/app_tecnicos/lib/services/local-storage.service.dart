import 'dart:convert';

import 'package:app_tecnicos/constants/constants.dart';
import 'package:localstorage/localstorage.dart';

final class LocalStorageService {
  static void save(String key, dynamic value) {
    final LocalStorage storage = LocalStorage(Constants.APP_NAME);

    storage.setItem(key, value);
  }

  static Map<String, dynamic> get(String key) {
    final LocalStorage storage = LocalStorage(Constants.APP_NAME);

    return json.decode(storage.getItem(key));
  }

  static void removeItemFromLocalStorage(String key) {
    final LocalStorage storage = LocalStorage(Constants.APP_NAME);

    storage.deleteItem(key);
  }
}
