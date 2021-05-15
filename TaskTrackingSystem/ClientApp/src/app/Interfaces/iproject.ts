export interface IProject {
  name: string;
  startTime: Date;
  endTime: Date;
  status: number;
  description: string;
  clientEmail: string;
  percentCompletion: number;
  editProject: { val: boolean };
  viewTasks: { val: boolean };
}
