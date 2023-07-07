class OrdemServicoModel {
  OrdemServicoModel({required this.codOS});

  final String codOS;

  factory OrdemServicoModel.fromJSON(Map<String, dynamic> json) =>
      OrdemServicoModel(codOS: json['codOS']);

  Map<String, dynamic> toJSON() => {"codOS": codOS};
}
