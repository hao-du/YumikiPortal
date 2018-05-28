import { Task } from './task.model.js';

export class TaskPaging {
    Records: Task[];
    CurrentPage: number;
    ItemsPerPage: number;
    TotalItems: number;
}