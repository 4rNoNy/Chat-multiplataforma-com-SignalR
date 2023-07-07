import { Button, Col, OverlayTrigger, Row, Spinner, Tooltip } from "react-bootstrap";
import { useStore } from "../../stores/stores";
import { observer } from "mobx-react-lite";
import Post from "../../common/components/Post";
import { useEffect } from "react";
import InfiniteScroll from "react-infinite-scroll-component";



export default observer(function NewFeed() {
    const { modalStore, postStore } = useStore();
    const { loadPosts, posts, loadMore, hasMore } = postStore

    useEffect(() => {
        loadPosts().then(() => console.log('Post carregados com sucesso!'));
    }, [])

    return (
        <>
            <Row className="justify-content-center">

                <Col sm={8}>
                    <InfiniteScroll
                        dataLength={posts.length} //Este é um campo importante para renderizar os próximos dados
                        next={loadMore}
                        hasMore={hasMore}
                        loader={<div className="center-loading"><Spinner animation="border" variant="primary" /></div>}
                        endMessage={
                            <p style={{ textAlign: 'center' }}>
                                <b>Uau! você já viu tudo</b>
                            </p>
                        }>
                        {posts.map((post, index) => (
                            <Post key={index} token="" post={post} />
                        ))}
                    </InfiniteScroll>
                </Col>
            </Row>
        </>
    )
})