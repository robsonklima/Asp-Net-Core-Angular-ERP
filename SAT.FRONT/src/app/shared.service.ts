import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class SharedService {

    private _listEvents: SharedData[] = [];

    clearListEvents() {
        this._listEvents = [];
    }

    sendClickEvent(sendClickFromComponent: any, params?: any[]) {
        let event = this._listEvents.find(events => events.ComponentTrigger == sendClickFromComponent);
        if (event) {
            event.Params = params;
            event.Event.next(params);
        }
    }

    getClickEvent(getClickFromComponent: any): Observable<any> {
        if (!this._listEvents.find(events => events.ComponentTrigger == getClickFromComponent)) {
            let data = new SharedData();
            data.ComponentTrigger = getClickFromComponent;
            data.Event = new Subject<any>();

            this._listEvents.push(data);
            return this._listEvents.find((element) => element == data).Event.asObservable();
        }
    }

    listenEvent(fromComponent: any): Observable<any> {
        return this.getClickEvent(fromComponent);
    }

    invokeEvent(toComponent: any, params?: any[]) {
        return this.sendClickEvent(toComponent, params);
    }
}

export class SharedData {
    ComponentTrigger: any;
    Event: Subject<any>;
    Params?: any[];
}