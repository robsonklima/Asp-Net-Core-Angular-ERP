class RelatorioAtendimentoModel {
  RelatorioAtendimentoModel({required this.codRAT});

  final String codRAT;

  factory RelatorioAtendimentoModel.fromJSON(Map<String, dynamic> json) =>
      RelatorioAtendimentoModel(codRAT: json['codRAT']);

  Map<String, dynamic> toJSON() => {"codRAT": codRAT};
}
