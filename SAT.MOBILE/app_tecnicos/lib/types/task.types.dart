class SatTask {
  late int codSatTask;
  late String status;
  late DateTime dataHoraCad;
  late DateTime dataHoraProcessamento;
  late int codSatTaskTipo;

  Map<String, dynamic> toJSON() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data["codSatTask"] = codSatTask;
    data["status"] = status;
    data['dataHoraCad'] = dataHoraCad;
    data['dataHoraProcessamento'] = dataHoraProcessamento;
    return data;
  }
}
