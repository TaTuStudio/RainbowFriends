/**
 * Copyright (c) 2014-present, Facebook, Inc. All rights reserved.
 *
 * You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
 * copy, modify, and distribute this software in source code or binary form for use
 * in connection with the web services and APIs provided by Facebook.
 *
 * As with any software that integrates with the Facebook platform, your use of
 * this software is subject to the Facebook Developer Principles and Policies
 * [http://developers.facebook.com/policy/]. This copyright notice shall be
 * included in all copies or substantial portions of the software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.Collections.Generic;
using UnityEngine;

namespace Facebook.Unity.Example
{
    internal sealed class MainMenu : MenuBase
    {

        enum Scope {
            PublicProfile   = 1,
            UserFriends     = 2,
            UserBirthday    = 3,
            UserAgeRange    = 4,
            PublishActions  = 5,
            UserLocation    = 6,
            UserHometown    = 7,
            UserGender      = 8,
        }

        protected override bool ShowBackButton()
        {
            return false;
        }

        protected override void GetGui()
        {
            GUILayout.BeginVertical();


            bool enabled = GUI.enabled;
            if (Button("FB.Init"))
            {
                FB.Init(OnInitComplete, OnHideUnity);
                Status = "FB.Init() called with " + FB.AppId;
            }

            GUILayout.BeginHorizontal();

            GUI.enabled = enabled && FB.IsInitialized;
            if (Button("Classic login"))
            {
                CallFBLogin(LoginTracking.ENABLED, new HashSet<Scope> { Scope.PublicProfile });
                Status = "Classic login called";
            }
            if (Button("Get publish_actions"))
            {
                CallFBLoginForPublish();
                Status = "Login (for publish_actions) called";
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (Button("Limited login"))
            {
                CallFBLogin(LoginTracking.LIMITED, new HashSet<Scope> { Scope.PublicProfile });
                Status = "Limited login called";

            }
            if (Button("Limited login +friends"))
            {
                CallFBLogin(LoginTracking.LIMITED, new HashSet<Scope> { Scope.PublicProfile, Scope.UserFriends });
                Status = "Limited login +friends called";

            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (Button("Limited Login+bday"))
            {
                CallFBLogin(LoginTracking.LIMITED, new HashSet<Scope> { Scope.PublicProfile, Scope.UserBirthday });
                Status = "Limited login +bday called";
            }

            if (Button("Limited Login+agerange"))
            {
                CallFBLogin(LoginTracking.LIMITED, new HashSet<Scope> { Scope.PublicProfile, Scope.UserAgeRange });
                Status = "Limited login +agerange called";
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (Button("Limited Login + location"))
            {
                CallFBLogin(LoginTracking.LIMITED, new HashSet<Scope> { Scope.PublicProfile, Scope.UserLocation });
            }

            if (Button("Limited Login + Hometown"))
            {
                CallFBLogin(LoginTracking.LIMITED, new HashSet<Scope> { Scope.PublicProfile, Scope.UserHometown });
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (Button("Limited Login + Gender"))
            {
                CallFBLogin(LoginTracking.LIMITED, new HashSet<Scope> { Scope.PublicProfile, Scope.UserGender });
            }


            GUI.enabled = FB.IsLoggedIn;


            // Fix GUILayout margin issues
            GUILayout.Label(GUIContent.none, GUILayout.MinWidth(MarginFix));
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();

            // Fix GUILayout margin issues
            GUILayout.Label(GUIContent.none, GUILayout.MinWidth(MarginFix));
            GUILayout.EndHorizontal();

            #if !UNITY_WEBGL
            if (Button("Logout"))
            {
                CallFBLogout();
                Status = "Logout called";
            }
            #endif

            GUI.enabled = enabled && FB.IsInitialized;
            if (Button("Share Dialog"))
            {
                SwitchMenu(typeof(DialogShare));
            }

            if (Button("App Requests"))
            {
                SwitchMenu(typeof(AppRequests));
            }

            if (Button("Graph Request"))
            {
                SwitchMenu(typeof(GraphRequest));
            }

            if (Constants.IsWeb && Button("Pay"))
            {
                SwitchMenu(typeof(Pay));
            }

            if (Button("App Events"))
            {
                SwitchMenu(typeof(AppEvents));
            }

            if (Button("App Links"))
            {
                SwitchMenu(typeof(AppLinks));
            }

            if (Button("Tournaments"))
            {
                SwitchMenu(typeof(TournamentsMenu));
            }

            if (Constants.IsMobile && Button("Access Token"))
            {
                SwitchMenu(typeof(AccessTokenMenu));
            }

            if (Button("UploadToMediaLibrary"))
            {
                SwitchMenu(typeof(UploadToMediaLibrary));
            }
            
            GUILayout.EndVertical();

            GUI.enabled = enabled;
        }

        private void CallFBLogin(LoginTracking mode, HashSet<Scope> scope)
        {
            List<string> scopes = new List<string>();

            if(scope.Contains(Scope.PublicProfile)) {
                scopes.Add("public_profile");
            }
            if(scope.Contains(Scope.UserFriends))
            {
                scopes.Add("user_friends");
            }
            if(scope.Contains(Scope.UserBirthday))
            {
                scopes.Add("user_birthday");
            }
            if(scope.Contains(Scope.UserAgeRange))
            {
                scopes.Add("user_age_range");
            }

            if(scope.Contains(Scope.UserLocation))
            {
                scopes.Add("user_location");
            }

            if(scope.Contains(Scope.UserHometown))
            {
                scopes.Add("user_hometown");
            }

            if(scope.Contains(Scope.UserGender))
            {
                scopes.Add("user_gender");
            }


            if (mode == LoginTracking.ENABLED)
            {
                if (Constants.CurrentPlatform == FacebookUnityPlatform.IOS) {
                    FB.Mobile.LoginWithTrackingPreference(LoginTracking.ENABLED, scopes, "classic_nonce123", HandleResult);
                } else {
                    FB.LogInWithReadPermissions(scopes, HandleResult);
                }
            }
            else // mode == loginTracking.LIMITED
            {
                FB.Mobile.LoginWithTrackingPreference(LoginTracking.LIMITED, scopes, "limited_nonce123", HandleLimitedLoginResult);
            }

        }

        private void CallFBLoginForPublish()
        {
            // It is generally good behavior to split asking for read and publish
            // permissions rather than ask for them all at once.
            //
            // In your own game, consider postponing this call until the moment
            // you actually need it.
            FB.LogInWithPublishPermissions(new List<string> { "publish_actions" }, HandleResult);
        }

        private void CallFBLogout()
        {
            FB.LogOut();
        }

        private void OnInitComplete()
        {
            Status = "Success - Check log for details";
            LastResponse = "Success Response: OnInitComplete Called\n";
            string logMessage = string.Format(
                "OnInitCompleteCalled IsLoggedIn='{0}' IsInitialized='{1}'",
                FB.IsLoggedIn,
                FB.IsInitialized);
            LogView.AddLog(logMessage);
            if (AccessToken.CurrentAccessToken != null)
            {
                LogView.AddLog(AccessToken.CurrentAccessToken.ToString());
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            Status = "Success - Check log for details";
            LastResponse = string.Format("Success Response: OnHideUnity Called {0}\n", isGameShown);
            LogView.AddLog("Is game shown: " + isGameShown);
        }
    }
}
