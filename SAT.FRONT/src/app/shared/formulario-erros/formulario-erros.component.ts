import { Component, Input } from '@angular/core';
import { FormGroup, ValidationErrors } from '@angular/forms';

@Component({
    selector: 'app-formulario-erros',
    templateUrl: './formulario-erros.component.html'
})
export class FormularioErrosComponent {
    @Input() form: FormGroup;

    constructor() {}

    showFormErrors(): any {

		const errors: any = [];

        if (!this.form)
            return;

		Object.keys(this.form.controls).forEach(key => {
			const controlErrors: ValidationErrors = this.form.get(key).errors;
			if (controlErrors != null) {
				Object.keys(controlErrors).forEach(keyError => {
					errors.push({
						control: this.formatKey(key),
						error: this.formatError(keyError),
						status: controlErrors[keyError]
					});
				});
			}
		});

		return errors;
	}

	formatKey(key: string): string {
		if (!key.length || key.length == 1) 
			return

		return (key.charAt(0).toUpperCase() + key.slice(1).replace(/([A-Z])/g, ' $1').trim());
	}

	formatError(error: string): string {
		switch (error) {
			case 'required':
				return 'Favor preencher o campo '
			case 'maxlength':
				return 'Tamanho do campo excedido para '
			default:
				return error;
		}
	}
}