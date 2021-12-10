import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, ReplaySubject } from 'rxjs';
import { Notificacao } from 'app/layout/common/notifications/notifications.types';
import { map, switchMap, take, tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class NotificationsService
{
    private _notifications: ReplaySubject<Notificacao[]> = new ReplaySubject<Notificacao[]>(1);

    constructor(
        private _httpClient: HttpClient
    ) {}
    
    get notifications$(): Observable<Notificacao[]>
    {
        return this._notifications.asObservable();
    }
    
    getAll(): Observable<Notificacao[]>
    {
        return this._httpClient.get<Notificacao[]>('api/common/notifications').pipe(
            tap((notifications) => {
                this._notifications.next(notifications);
            })
        );
    }
    
    create(notification: Notificacao): Observable<Notificacao>
    {
        return this.notifications$.pipe(
            take(1),
            switchMap(notifications => this._httpClient.post<Notificacao>('api/common/notifications', {notification}).pipe(
                map((newNotification) => {

                    // Update the notifications with the new notification
                    this._notifications.next([...notifications, newNotification]);

                    // Return the new notification from observable
                    return newNotification;
                })
            ))
        );
    }
    
    update(codNotificacao: number, notification: Notificacao): Observable<Notificacao>
    {
        return this.notifications$.pipe(
            take(1),
            switchMap(notifications => this._httpClient.patch<Notificacao>('api/common/notifications', {
                codNotificacao,
                notification
            }).pipe(
                map((updatedNotification: Notificacao) => {

                    // Find the index of the updated notification
                    const index = notifications.findIndex(item => item.codNotificacao === codNotificacao);

                    // Update the notification
                    notifications[index] = updatedNotification;

                    // Update the notifications
                    this._notifications.next(notifications);

                    // Return the updated notification
                    return updatedNotification;
                })
            ))
        );
    }
    
    delete(codNotificacao: number): Observable<boolean>
    {
        return this.notifications$.pipe(
            take(1),
            switchMap(notifications => this._httpClient.delete<boolean>('api/common/notifications', {params: {codNotificacao}}).pipe(
                map((isDeleted: boolean) => {

                    // Find the index of the deleted notification
                    const index = notifications.findIndex(item => item.codNotificacao === codNotificacao);

                    // Delete the notification
                    notifications.splice(index, 1);

                    // Update the notifications
                    this._notifications.next(notifications);

                    // Return the deleted status
                    return isDeleted;
                })
            ))
        );
    }
    
    markAllAsRead(): Observable<boolean>
    {
        return this.notifications$.pipe(
            take(1),
            switchMap(notifications => this._httpClient.get<boolean>('api/common/notifications/mark-all-as-read').pipe(
                map((isUpdated: boolean) => {

                    // Go through all notifications and set them as read
                    notifications.forEach((notification, index) => {
                        notifications[index].lida = 1;
                    });

                    // Update the notifications
                    this._notifications.next(notifications);

                    // Return the updated status
                    return isUpdated;
                })
            ))
        );
    }
}
