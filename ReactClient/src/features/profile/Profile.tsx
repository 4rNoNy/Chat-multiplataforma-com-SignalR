import { useEffect, useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import agent from "../../api/agent";
import Post from "../../common/components/Post";
import { IUser } from "../../models/user";
import '../../css/profile.css';

export default function Profile() {
    const [token, setToken] = useState('');

    async function addPost() {
        const formData = new FormData();
        formData.append('content', 'Você criou um novo post');
        await agent.Posts.addPost(formData);
    }

    useEffect(() => {
        let user: IUser | null = JSON.parse(localStorage.getItem('user')!);
        if (user) {
            setToken(user.token);
        }
    }, [token]);

    return (
        <>
            <div className="header-profile">
                <div className="text-center" style={{ position: 'relative' }}>
                    <img src="https://img.freepik.com/vetores-gratis/fundo-de-wireframe-geometrico-abstrato_52683-59421.jpg?w=2000" className="rounded fit-img" alt="Fundo Perfil" />
                    <div className="img-block">
                        <img src="/assets/user0.jpg" className="rounded-circle" style={{ height: 150,width: 150, border: '4px solid green' }} />
                    </div>
                </div>
                <h2 className="text-center text-primary name-profile">Gian das P4r4d4s</h2>
            </div>
            <div className="body-profile">
                <Row className="crow">
                    <Col md={4}>
                        <div className="card card-info">
                            <div className="card-header text-primary">Informação</div>
                            <div className="card-body">
                                <div>
                                    <strong>Nome do usuário:</strong>
                                    <p>Gian das P4r4d4s</p>
                                </div>
                                <div>
                                    <strong>Online pela última vez:</strong>
                                    <p>1 hora atrás</p>
                                </div>
                                <div>
                                    <strong>Data de criação:</strong>
                                    <p>12/05/2023</p>
                                </div>
                            </div>
                        </div>
                    </Col>
                    <Col className="button-post">
                        <Button variant="primary" href="/new-feed" onClick={addPost}>
                            Novo Post
                        </Button>
                    </Col>
                </Row>
            </div>
        </>
    );
}
