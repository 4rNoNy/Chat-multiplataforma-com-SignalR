import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Card, ListGroup } from "react-bootstrap";
import { useStore } from "../../../stores/stores";
import ChatBox from "./ChatBox";
import { library } from '@fortawesome/fontawesome-svg-core';
import { faEdit, faToggleOn } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import MiniChatBox from "./Mini-chatBox";
import { IMember } from "../../../models/user";
import { messageService } from "../../services/messageService";
import '../../../css/FriendList.css';

library.add(faEdit, faToggleOn);

export default observer(function FriendList() {
    const {
        userOnlineStore: {
            userChatBox,
            miniChatBox,
            setUserChatBox,
            addUserChatBox
        },
        commonStore: { hide, toogleHide },
        presenceHubStore
    } = useStore();

    useEffect(() => {
        const userTempChatBox: IMember[] = JSON.parse(localStorage.getItem('chatboxusers')!);
        setUserChatBox(userTempChatBox);

        const subscription = messageService.getMessage().subscribe((member: IMember) => {
            addUserChatBox(member);
        });

        return () => {
            subscription.unsubscribe();
        }
    }, []);

    return (
        <>
            <Card
                className={`friend-list-card ${hide ? 'hidden' : ''}`}
                style={{
                    maxWidth: '18rem',
                    position: 'fixed',
                    right: '25px',
                    bottom: '1px',
                    zIndex: 222
                }}
            >
                <Card.Header className="friend-list-card-header">
                    Usuários online ({presenceHubStore.usersOnline.length})
                </Card.Header>
                <Card.Body className="friend-list-card-body">
                    <ListGroup variant="flush">
                        {presenceHubStore.usersOnline.map((member, index) => (
                            <ListGroup.Item
                                key={index}
                                className="friend-list-item"
                                onClick={() => addUserChatBox(member.member!)}
                            >
                                {member.member!.displayName}
                            </ListGroup.Item>
                        ))}
                    </ListGroup>
                </Card.Body>
            </Card>
            <div className="chat-boxes">
                {userChatBox.map((user, index) => (
                    <ChatBox key={index} user={user} />
                ))}
            </div>

            <div style={{ position: 'relative' }}>
                <div className="mini-list d-flex flex-column">
                    {miniChatBox.map((user, index) => (
                        <MiniChatBox key={index} user={user} />
                    ))}
                    <div style={{ margin: 5 }}>
                        <div
                            onClick={toogleHide}
                            className="edit d-flex align-items-center justify-content-center rounded-circle"
                        >
                            <FontAwesomeIcon icon={faToggleOn} />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
});
