import { FormControl } from "@angular/forms";

export function EmailValidator(): any {
  return (control: FormControl): { [key: string]: any } => {
    const validation = String(control.value)
      .toLowerCase()
      .match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      );

    if (!validation) {
      return { invalidFormat: true };
    }

    return null;
  };
}