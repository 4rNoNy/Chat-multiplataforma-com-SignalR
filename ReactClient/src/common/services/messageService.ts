import { Subject } from 'rxjs';
import { IMember } from '../../models/user';

const subject = new Subject<IMember>();
export const messageService = {
    sendMessage: (user: IMember) => subject.next(user),
    getMessage: () => subject.asObservable()
};