import { OnTimeBase } from './ontimebase.model.js';

export class Project extends OnTimeBase {
    ProjectName: string;
    Prefix: string;
    //For User Assignment Purpose to check if user is assigned to specific project
    IsAssigned: boolean;
}
