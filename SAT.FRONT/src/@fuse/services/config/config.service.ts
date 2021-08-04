import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { merge } from 'lodash-es';
import { FUSE_APP_CONFIG } from '@fuse/services/config/config.constants';

@Injectable({
    providedIn: 'root'
})
export class FuseConfigService
{
    private _config: BehaviorSubject<any>;

    constructor(@Inject(FUSE_APP_CONFIG) config: any)
    {
        this._config = new BehaviorSubject(config);
    }
    
    set config(value: any)
    {
        const config = merge({}, this._config.getValue(), value);
        this._config.next(config);
    }

    get config$(): Observable<any>
    {
        return this._config.asObservable();
    }

    reset(): void
    {
        this._config.next(this.config);
    }
}
