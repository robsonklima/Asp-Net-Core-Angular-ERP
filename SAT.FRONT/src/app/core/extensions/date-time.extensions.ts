import { Injectable } from "@angular/core";
import moment from "moment";

@Injectable({
    providedIn: 'root'
})

export class DateTimeExtensions
{
    constructor () { }

    public getTimeFromMins(mins)
    {
        var h = mins / 60 | 0,
            m = mins % 60 | 0;
        return moment.utc().hours(h).minutes(m).format("HH:mm");
    }
}