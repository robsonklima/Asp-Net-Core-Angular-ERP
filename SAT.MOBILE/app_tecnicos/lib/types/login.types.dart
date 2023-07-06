import 'package:app_tecnicos/types/usuario.types.dart';

class LoginModel {
  LoginModel({required this.codUsuario, required this.senha});

  final String codUsuario;
  final String senha;

  factory LoginModel.fromJSON(Map<String, dynamic> json) =>
      LoginModel(codUsuario: json['codUsuario'], senha: json['senha']);

  Map<String, dynamic> toJSON() => {
        "codUsuario": codUsuario,
        "senha": senha,
      };
}

class UsuarioRetornoModel {
  UsuarioRetornoModel({required this.usuario, required this.token});

  final UsuarioModel usuario;
  final String token;

  factory UsuarioRetornoModel.fromJSON(Map<String, dynamic> json) =>
      UsuarioRetornoModel(usuario: json['usuario'], token: json['token']);

  Map<String, dynamic> toJSON() => {
        "usuario": usuario,
        "token": token,
      };
}
