class UsuarioModel {
  UsuarioModel({required this.codUsuario, required this.nomeUsuario});

  final String codUsuario;
  final String nomeUsuario;

  factory UsuarioModel.fromJSON(Map<String, dynamic> json) => UsuarioModel(
      codUsuario: json['codUsuario'], nomeUsuario: json['nomeUsuario']);

  Map<String, dynamic> toJSON() => {
        "codUsuario": codUsuario,
        "nomeUsuario": nomeUsuario,
      };
}
