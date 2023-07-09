import 'package:app_tecnicos/types/perfil.types.dart';
import 'package:app_tecnicos/types/tecnico.types.dart';

class UsuarioModel {
  UsuarioModel(
      {required this.codUsuario,
      required this.nomeUsuario,
      required this.tecnico,
      required this.perfil});

  final String codUsuario;
  final String nomeUsuario;
  final PerfilModel perfil;
  final TecnicoModel tecnico;

  factory UsuarioModel.fromJSON(Map<String, dynamic> json) => UsuarioModel(
        codUsuario: json['codUsuario'],
        nomeUsuario: json['nomeUsuario'],
        perfil: PerfilModel.fromJSON(json['perfil']),
        tecnico: TecnicoModel.fromJSON(json['tecnico']),
      );

  Map<String, dynamic> toJSON() => {
        "codUsuario": codUsuario,
        "nomeUsuario": nomeUsuario,
      };
}
