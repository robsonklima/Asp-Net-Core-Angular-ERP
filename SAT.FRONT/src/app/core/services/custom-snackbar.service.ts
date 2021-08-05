import { Injectable } from '@angular/core';
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
    providedIn: 'root'
})
export class CustomSnackbarService {
    constructor(
        public snackBar: MatSnackBar
    ) { }

    public exibirAlerta(message, action: string='Ok') {
        this.snackBar.open(message, action, {});
    }

    public exibirToast(message: string, type: string="neutral", duration: number=3000): void {
        this.snackBar.open(message, '', {
            duration: duration,
            horizontalPosition: "right",
            verticalPosition: "top",
            panelClass: [type]
        });
    }
}