import { Task } from './task.model.js';

export class PagingTask {
    Records: Task[];
    CurrentPage: number;
    ItemsPerPage: number;
    TotalItems: number;
    DefaultPhaseID: string;
    DefaultProjectID: string;
    DefaultTaskID: string;
}