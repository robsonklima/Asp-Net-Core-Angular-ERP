import { Injectable } from "@angular/core";
import moment from "moment";

@Injectable({
    providedIn: 'root'
})
export class Utils {
    constructor() {}

    obterDataBD(dataHora: string=null): string {
        if (dataHora)
            return moment(dataHora).format('YYYY-MM-DD HH:mm:ss');
        
        return moment().format('YYYY-MM-DD HH:mm:ss');
    }

    obterDuracao(dataHora: string): string {
        if (!dataHora)
            return '';
        
        return moment(dataHora).locale('pt').fromNow();
    }
}