import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'lowercase' })
export class LowercasePipe implements PipeTransform {

  transform(value: string) {
    if (value) {
      return value.toLowerCase();
    }
    return value;
  }
}