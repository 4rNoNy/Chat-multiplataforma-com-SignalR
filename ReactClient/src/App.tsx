import './App.css';
import { ToastContainer } from 'react-toastify';
import ModalContainer from './common/modals/ModalContainer';
import { Button, Container } from 'react-bootstrap';
import MenuBar from './common/components/MenuBar';
import { Outlet } from 'react-router-dom';
import { useStore } from './stores/stores';
import { observer } from 'mobx-react-lite';
import { useEffect, useRef } from 'react';
import FriendList from './common/components/friendList/FriendList';

function App() {
  const { userStore, presenceHubStore,  audioStore, modalStore } = useStore();
  const buttonRef = useRef<any>(null);
  const buttonCallOneToOneRef = useRef<any>(null);

  useEffect(() => {
    if (userStore.user) {
      presenceHubStore.createHubConnection(userStore.user);
    }

  }, [userStore])

  return (
    <>
      <Button style={{ display: 'none' }} onClick={audioStore.toogle} ref={buttonRef}>Play</Button>
      <ToastContainer position='bottom-right' hideProgressBar theme='colored' />
      <ModalContainer />
      <MenuBar />
      <Container>
        <Outlet />
      </Container>

      {userStore.isLoggedIn ? (
        <div style={{ position: 'relative' }}>
          <FriendList />
        </div>
      ) : null}
    </>
  );
}

export default observer(App);
