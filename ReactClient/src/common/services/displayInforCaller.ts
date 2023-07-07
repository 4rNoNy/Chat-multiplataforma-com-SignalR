import { Subject } from 'rxjs';
import { IMember } from '../../models/user';

const subject = new Subject<IMember>();
// usado para exibir a caixa de diálogo de chamada recebida, defina download app.tsx
export const callerMessageService = {
    sendMessage: (user: IMember) => subject.next(user),
    getMessage: () => subject.asObservable()
};