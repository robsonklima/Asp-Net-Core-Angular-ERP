class TecnicoModel {
  TecnicoModel({required this.codTecnico, required this.nome});

  final int codTecnico;
  final String nome;

  factory TecnicoModel.fromJSON(Map<String, dynamic> json) =>
      TecnicoModel(codTecnico: json['codTecnico'], nome: json['nome']);

  Map<String, dynamic> toJSON() => {
        "codTecnico": codTecnico,
        "nome": nome,
      };
}
