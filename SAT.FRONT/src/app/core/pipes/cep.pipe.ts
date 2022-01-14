import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'cep' })
export class CEPPipe implements PipeTransform
{
    transform(value)
    {
        if (value !== null)
        {
            const v = value.toString().replace(/\D/g, '');
            if (v.length === 8)
                return v.slice(0, 5) + "-" + v.toString().slice(5);

            return v;
        }
    }
}