import { QueryStringParameters } from "./generic.types";

export interface IISLog
{
    dateTimeEvent: string;
    sSitename?: string;
    sComputername?: string;
    sIp?: string;
    csMethod: string;
    csUriStem: string;
    csUriQuery?: string;
    sPort: number;
    csUsername?: string;
    cIp: string;
    csVersion?: string;
    csUserAgent: string;
    csCookie?: string;
    csReferer?: string;
    csHost?: string;
    scStatus: number;
    scSubstatus: number;
    scWin32Status: number;
    scBytes?: string;
    csBytes?: string;
    timeTaken: number;
}

export interface IISLogParameters extends QueryStringParameters {
    data?: string;
};

