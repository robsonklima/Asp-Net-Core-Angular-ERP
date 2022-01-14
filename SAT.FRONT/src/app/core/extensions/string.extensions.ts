import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class StringExtensions
{
    constructor () { }

    public isEmptyOrWhiteSpace(text: string): boolean
    {
        return text == null || text.match(/^\s*$/) !== null;
    }
}