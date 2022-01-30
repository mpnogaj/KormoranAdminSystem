import { useParams } from "react-router"

const withParams = (Component: any) => (props: any) => {
    const params = useParams();
    console.log(params);
    return (
        <Component {...props} params={params}/>
    );
}

export default withParams;