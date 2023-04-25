import { Injectable } from "@angular/core";
import _ from "lodash";
import moment from "moment";

@Injectable({
  providedIn: 'root'
})
export class Utils {
  constructor() { }

  obterDataBD(dataHora: string = null): string {
    if (dataHora)
      return moment(dataHora).format('YYYY-MM-DD HH:mm:ss');

    return moment().format('YYYY-MM-DD HH:mm:ss');
  }

  removerAcentos(str: string='') {
    return str.normalize('NFD').replace(/[\u0300-\u036f]/g, "");
  }

  obterDuracao(dataHora: string): string {
    if (!dataHora)
      return '';

    return moment(dataHora).locale('pt').fromNow();
  }

  toCamelCase(text: string): string {
    text = text.toLowerCase();

    return _.startCase(text);
  }

  hasNumbers = (str) => {
    if (str.match(/\d+/g) !== null) {
      return true;
    } else {
      return false;
    }
  };

  obterExtensionBase64(base64: string) {
    switch (base64?.substring(0, 5)?.toUpperCase()) {
      case "IVBOR":
        return "png";
      case "/9J/4":
        return "jpg";
      case "AAAAF":
        return "mp4";
      case "JVBER":
        return "pdf";
      case "AAABA":
        return "ico";
      case "UMFYI":
        return "rar";
      case "E1XYD":
        return "rtf";
      case "U1PKC":
        return "txt";
      case "MQOWM":
      case "77U/M":
        return "srt";
      case "0M8R4":
        return "xls";
      case "UESDB":
        return "xlsx";
      default:
        return '';
    }
  }
}