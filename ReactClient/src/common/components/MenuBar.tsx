import { observer } from "mobx-react-lite";
import { Container, Dropdown, Nav, Navbar } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useStore } from "../../stores/stores";
import '../../css/MenuBar.css';

export default observer(function MenuBar() {
    const { userStore } = useStore();
    const { isLoggedIn, user, logout } = userStore;
    return (
        <>
            <Navbar bg="dark" variant="dark">
                <Container className="Container-Navbar">
                    <a href="/">
                        <img src="https://uploads-ssl.webflow.com/5e9cdc9e9aae7e09dbbc7b72/5e9e5180af4cb38ddc2437be_chatapp.svg" alt="#" className="image"></img>
                    </a>
                    <Nav className="me-auto">
                        <Nav.Link as={Link} to="/">Home</Nav.Link>
                        <Nav.Link as={Link} to="/new-feed">Comunidade</Nav.Link>
                        <Nav.Link as={Link} to="/profile">Perfil</Nav.Link>
                        <Nav.Link as={Link} to="/login">Login</Nav.Link>
                    </Nav>
                    {isLoggedIn ? (
                        <div className="d-none d-sm-block">
                            <Dropdown>
                                <Dropdown.Toggle id="dropdown-button-dark-example1" variant="dark">
                                    <a className="img user">
                                        <img src="/assets/user.png" height="25" alt="img user" className="rounded" />
                                    </a>
                                    {user?.displayName}
                                </Dropdown.Toggle>
                                <Dropdown.Menu variant="dark">
                                    <Dropdown.Item as={Link} to={`/profile`}>
                                        Meu Perfil
                                    </Dropdown.Item>
                                    <Dropdown.Divider />
                                    <Dropdown.Item onClick={logout}>Sair</Dropdown.Item>
                                </Dropdown.Menu>
                            </Dropdown>
                        </div>
                    ) : null}
                </Container>
            </Navbar>
        </>
    );
});
