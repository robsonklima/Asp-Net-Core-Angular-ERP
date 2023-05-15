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

  isPOS(codEquip: number): boolean {
    return ['85','96','97','107','134','147','153','157','158','172','204','268','289','397','398','399','400','401','856','1098','1121','1123','1126','1146','1199']
      .includes(codEquip.toString());
  }

  dynamicSort(property) {
    var sortOrder = 1;
    if(property[0] === "-") {
        sortOrder = -1;
        property = property.substr(1);
    }
    return function (a, b) {
        var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
        return result * sortOrder;
    }
  }
}