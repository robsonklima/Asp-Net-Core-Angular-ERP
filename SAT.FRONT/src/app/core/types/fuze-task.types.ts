export interface FuzeTag
{
    id?: string;
    title?: string;
}

export interface FuzeTask
{
    id: string;
    type: 'task' | 'section';
    title: string;
    notes: string;
    completed: boolean;
    dueDate: string | null;
    priority: 0 | 1 | 2;
    tags: string[];
    order: number;
}
