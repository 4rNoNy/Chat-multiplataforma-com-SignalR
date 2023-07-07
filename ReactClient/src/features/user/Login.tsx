import { ErrorMessage, Formik, Form } from "formik";
import { observer } from "mobx-react-lite";
import { Button } from "react-bootstrap";
import * as yup from 'yup';
import MyTextInput from "../../common/form/MyTextInput";
import { useStore } from "../../stores/stores";
import '../../css/Login.css';

export default observer(function Login() {
    const { userStore } = useStore();

    const schema = yup.object().shape({
        username: yup.string().required(),
        password: yup.string().required(),
    });

    return (
        <div className="login-box">
            <p className="login-title">Login</p>
            <Formik
                validationSchema={schema}
                initialValues={{ username: '', password: '', error: null }}
                onSubmit={(value, { setErrors }) => userStore.login(value)
                    .catch(err => setErrors({ error: err }))}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty, errors }) => (
                    <form onSubmit={handleSubmit}>
                        <div className="user-box">
                            <MyTextInput name="username" type="text" placeholder="Login" label="" />
                        </div>
                        <div className="user-box">
                            <MyTextInput name="password" type="password" placeholder="Password" label="" />
                        </div>
                        <button className={`submit-button ${isSubmitting || !dirty || !isValid ? 'disabled' : ''}`} type="submit" disabled={isSubmitting || !dirty || !isValid}>
                            <span></span>
                            <span></span>
                            <span></span>
                            <span></span>
                            {isSubmitting ? 'Loading...' : 'Submit'}
                        </button>

                        <ErrorMessage name="error" render={() => <div className="text-danger">{errors.error}</div>} />
                    </form>
                )}
            </Formik>
            <p>Don't have an account? <a href="" className="a2">Sign up!</a></p>
        </div>
    );
})
