class PerfilModel {
  PerfilModel({required this.codPerfil, required this.nomePerfil});

  final int codPerfil;
  final String nomePerfil;

  factory PerfilModel.fromJSON(Map<String, dynamic> json) =>
      PerfilModel(codPerfil: json['codPerfil'], nomePerfil: json['nomePerfil']);

  Map<String, dynamic> toJSON() => {
        "codPerfil": codPerfil,
        "nomePerfil": nomePerfil,
      };
}
