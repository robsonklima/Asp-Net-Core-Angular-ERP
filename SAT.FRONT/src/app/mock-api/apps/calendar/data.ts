/* eslint-disable */
import * as moment from 'moment';
moment.locale('pt-br')

export const calendars = [
    {
        id     : '1a470c8e-40ed-4c2d-b590-a4f1f6ead6cc',
        title  : 'Técnico A',
        color  : 'bg-teal-500',
        visible: true
    },
    {
        id     : '5dab5f7b-757a-4467-ace1-305fe07b11fe',
        title  : 'Técnico B',
        color  : 'bg-indigo-500',
        visible: true
    },
    {
        id     : '09887870-f85a-40eb-8171-1b13d7a7f529',
        title  : 'Técnico C',
        color  : 'bg-pink-500',
        visible: true
    }
];
export const events = [
    {
        id         : '660f0dcd-48f8-4266-a89a-8ee0789c074a',
        calendarId : '1a470c8e-40ed-4c2d-b590-a4f1f6ead6cc',
        title      : '6599890',
        description: 'Descrição do evento!',
        start      : moment().date(10).hour(11).minute(0).second(0).millisecond(0).toISOString(),
        end        : moment().date(10).hour(12).minute(0).second(0).millisecond(0).toISOString(),
        duration   : null,
    },
    {
        id         : '7471b840-5efb-45da-9092-a0f04ee5617b',
        calendarId : '09887870-f85a-40eb-8171-1b13d7a7f529',
        title      : '6599876',
        description: 'Descrição do evento!',
        start      : moment().date(11).hour(9).minute(0).second(0).millisecond(0).toISOString(),
        end        : moment().date(11).hour(10).minute(0).second(0).millisecond(0).toISOString(),
        duration   : null,
    },
    {
        id         : '7471b840-5efb-45da-9092-a0f04ee5617i',
        calendarId : '09887870-f85a-40eb-8171-1b13d7a7f529',
        title      : '6599109',
        description: 'Descrição do evento!',
        start      : moment().date(6).hour(12).minute(0).second(0).millisecond(0).toISOString(),
        end        : moment().date(6).hour(13).minute(0).second(0).millisecond(0).toISOString(),
        duration   : null,
    },
    {
        id         : '7471b840-5efb-45da-9092-a0f04ee5617i',
        calendarId : '5dab5f7b-757a-4467-ace1-305fe07b11fe',
        title      : '6599980',
        description: 'Descrição do evento!',
        start      : moment().date(6).hour(12).minute(0).second(0).millisecond(0).toISOString(),
        end        : moment().date(6).hour(13).minute(0).second(0).millisecond(0).toISOString(),
        duration   : null,
    },
    {
        id         : '7471b840-5efb-45da-9092-a0f04ee5617p',
        calendarId : '1a470c8e-40ed-4c2d-b590-a4f1f6ead6cc',
        title      : '6599144',
        description: 'Descrição do evento!',
        start      : moment().date(6).hour(12).minute(0).second(0).millisecond(0).toISOString(),
        end        : moment().date(6).hour(13).minute(0).second(0).millisecond(0).toISOString(),
        duration   : null,
    }
];
export const exceptions = [];
export const settings = {
    dateFormat : 'LL',
    timeFormat : '24',
    startWeekOn: 1,
    
};
export const weekdays = [
    {
        abbr : 'S',
        label: 'Segunda',
        value: 'Se'
    },
    {
        abbr : 'T',
        label: 'Terça',
        value: 'Te'
    },
    {
        abbr : 'Q',
        label: 'Quarta',
        value: 'Qu'
    },
    {
        abbr : 'Q',
        label: 'Quinta',
        value: 'Qu'
    },
    {
        abbr : 'S',
        label: 'Sexta',
        value: 'Se'
    },
    {
        abbr : 'S',
        label: 'Sábado',
        value: 'Sa'
    },
    {
        abbr : 'D',
        label: 'Domingo',
        value: 'Do'
    }
];
