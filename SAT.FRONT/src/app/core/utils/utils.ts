import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class SATUtils {
    init() {

    }

    toFixed(value, precision) {
        var precision = precision || 0,
            power = Math.pow(10, precision),
            absValue = Math.abs(Math.round(value * power)),
            result = (value < 0 ? '-' : '') + String(Math.floor(absValue / power));

        if (precision > 0)
        {
            var fraction = String(absValue % power),
                padding = new Array(Math.max(precision - fraction.length, 0) + 1).join('0');
            result += '.' + padding + fraction;
        }

        return result;
    }

    checkExtension(str: string) {
        switch (str.toUpperCase())
        {
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