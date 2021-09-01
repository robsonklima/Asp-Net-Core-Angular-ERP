import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'ellipsis' })
export class EllipsisPipe implements PipeTransform {

  transform(value: any, size: number) {
    value = String(value);

    if (value.length <= size) 
        return value;
    
    var output = value.substring(0,(size || 12)); //+ '..';
    
    return output;
  }
}