import { FormControl } from "@angular/forms";
import * as moment from "moment";

export function TimeValidator(format = "HH:mm"): any {
  return (control: FormControl): { [key: string]: any } => {
    const val = moment(control.value, format, true);

    if (!val.isValid()) {
      return { invalidFormat: true };
    }

    return null;
  };
}