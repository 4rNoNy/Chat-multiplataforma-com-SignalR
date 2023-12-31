import { createContext, useContext } from "react";
import AudioStore from "./audioStore";
import CommonStore from "./commonStore";
import MessageHubStore from "./messageHubStore";
import ModalStore from "./modalStore";
import PostStore from "./postStore";
import PresenceHubStore from "./presenceHubStore";
import UserOnlineStore from "./userOnlineStore";
import UserStore from "./userStore";

interface Store {
    commonStore: CommonStore;
    userStore: UserStore;
    modalStore: ModalStore;
    presenceHubStore: PresenceHubStore;
    messageHubStore: MessageHubStore;
    audioStore: AudioStore;
    userOnlineStore: UserOnlineStore;
    postStore: PostStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    modalStore: new ModalStore(),
    audioStore: new AudioStore(),
    userOnlineStore: new UserOnlineStore(),
    presenceHubStore: new PresenceHubStore(),
    messageHubStore: new MessageHubStore(),
    postStore: new PostStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}