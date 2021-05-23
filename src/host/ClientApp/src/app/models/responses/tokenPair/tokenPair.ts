import RemoteToken from '../tokenPair/remoteToken';

export default class TokenPair {
    AuthenticationToken: RemoteToken | undefined;
    RefreshToken: RemoteToken | undefined;
}