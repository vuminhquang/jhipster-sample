import React, { useEffect } from "react";
import { REDIRECT_URL } from "app/shared/util/url-utils";

export const LoginRedirect = (props) => {
  useEffect(() => {
    localStorage.setItem(REDIRECT_URL, props.location.state.from.pathname);
    window.location.reload();
  });

  return null;
};

export default LoginRedirect;
