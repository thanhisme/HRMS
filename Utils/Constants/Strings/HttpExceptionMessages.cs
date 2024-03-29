﻿namespace Utils.Constants.Strings
{
    public static class HttpExceptionMessages
    {
        public static string INTERNAL_SERVER_ERROR { get; } = "Internal Server Error";

        public static string UNAUTHORIZED { get; } = "Unauthorized";

        public static string NOT_FOUND { get; } = "Not Found";

        public static string FORBIDDEN { get; } = "Forbidden";

        public static string CONTRAINT_ERRORS { get; } = "Contraint Errors";

        public static string VALIDATION_ERRORS { get; } = "Validation Errors";

        public static string UNAUTHENTICATED_USER_ONLY { get; } = "Unauthenticated user only";

        public static string INVALID_USERNAME_OR_PASSWORD { get; } = "Invalid username or password";

        public static string INVALID_REFRESH_TOKEN { get; } = "Invalid refresh token";

        public static string TOKEN_IN_BLACKLIST { get; } = "Token is in black list!";

        public static string PASSWORD_IS_NOT_CORRECT { get; } = "Password is not correct";

        public static string PASSWORD_HAS_CHANGED { get; } = "Password has recently changed! Please sign in again!";

        public static string PASSWORD_CONFIRM_IS_NOT_MATCH { get; } = "Password confirm is not match";

        public static string INVALID_EMAIL { get; } = "Invalid email";

        public static string RESET_PASSWORD_TOKEN_EXPIRED { get; } = "Reset password token expired";

        public static string INVALID_RESET_PASSWORD_TOKEN { get; } = "Invalid reset password token";
    }
}
