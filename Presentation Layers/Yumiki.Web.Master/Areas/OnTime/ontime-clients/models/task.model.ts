import { OnTimeBase } from './ontimebase.model.js';

export class Task extends OnTimeBase {
    TaskName: string;
    ProjectID: string;
    ProjectName: string;
    PhaseID: string;
    PhaseName: string;
    AssignedUserID: string;
    AssignedUserName: string;
    StartDate: string;
    EndDate: string;
    Status: string;
    StatusUI: string;
    TaskDescriptions: string;
}
