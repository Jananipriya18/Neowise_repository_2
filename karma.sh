#!/bin/bash
export CHROME_BIN=/usr/bin/chromium
if [ ! -d "/home/coder/project/workspace/angularapp" ]
then
    cp -r /home/coder/project/workspace/karma/angularapp /home/coder/project/workspace/;
fi

if [ -d "/home/coder/project/workspace/angularapp" ]
then
    echo "project folder present"
    cp /home/coder/project/workspace/karma/karma.conf.js /home/coder/project/workspace/angularapp/karma.conf.js;

    # checking for usernav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/usernav" ]
    then
        cp /home/coder/project/workspace/karma/usernav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/usernav/usernav.component.spec.ts;
    else
        echo "Frontend_should_create_usernav_component FAILED";
    fi

    # checking for userviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/userviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userviewfeedback/userviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_userviewfeedback_component FAILED";
        echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
    fi

     # checking for adminnav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminnav" ]
    then
        cp /home/coder/project/workspace/karma/adminnav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminnav/adminnav.component.spec.ts;
    else
        echo "Frontend_should_create_adminnav_component FAILED";
    fi

    # checking for viewemployees.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/viewemployees" ]
    then
        cp /home/coder/project/workspace/karma/viewemployees.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/viewemployees/viewemployees.component.spec.ts;
    else
        echo "Frontend_should_create_viewemployees_component FAILED";
        echo "Frontend_should_contain_view_employees_heading_in_the_viewemployees_component FAILED";
    fi

    # checking for auth.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/auth.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/auth.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/auth.service.spec.ts;
    else
        echo "Frontend_should_create_auth_service FAILED";
    fi

    # checking for leave.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/leave.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/leave.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/leave.service.spec.ts;
    else
        echo "Frontend_should_create_leave_service FAILED";
    fi

    # checking for viewleave.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/viewleave" ]
    then
        cp /home/coder/project/workspace/karma/viewleave.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/viewleave/viewleave.component.spec.ts;
    else
        echo "Frontend_should_create_viewleave_component FAILED";
        echo "Frontend_should_contain_leave_requests_for_approval_heading_in_the_viewleave_component FAILED";
    fi

    # checking for error.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/error" ]
    then
        cp /home/coder/project/workspace/karma/error.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/error/error.component.spec.ts;
    else
        echo "Frontend_should_create_error_component FAILED";
        echo "Frontend_should_contain_wrong_message_in_the_error_component FAILED";
    fi

    # checking for feedback.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/employee.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/feedback.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/employee.service.spec.ts;
    else
        echo "Frontend_should_create_employee_service FAILED";
    fi

     # checking for wfh.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/wfh.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/wfh.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/wfh.service.spec.ts;
    else
        echo "Frontend_should_create_wfh_service FAILED";
    fi

    # checking for home.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/home" ]
    then
        cp /home/coder/project/workspace/karma/home.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/home/home.component.spec.ts;
    else
        echo "Frontend_should_create_home_component FAILED";
        echo "Frontend_should_contain_workbuddy_heading_in_the_home_component FAILED";
    fi

    # checking for loan.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/feedback.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/loan.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/feedback.service.spec.ts;
    else
        echo "Frontend_should_create_feedback_service FAILED";
    fi

    # checking for viewwfh.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/viewwfh" ]
    then
        cp /home/coder/project/workspace/karma/viewwfh.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/viewwfh/viewwfh.component.spec.ts;
    else
        echo "Frontend_should_create_viewwfh_component FAILED";
        echo "Frontend_should_contain_wfh_requests_for_approval_heading_in_the_viewwfh_component FAILED";
    fi

    # checking for login.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/login" ]
    then
        cp /home/coder/project/workspace/karma/login.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/login/login.component.spec.ts;
    else
        echo "Frontend_should_create_login_component FAILED";
        echo "Frontend_should_contain_login_heading_in_the_login_component FAILED";
    fi

    # checking for navbar.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/navbar" ]
    then
        cp /home/coder/project/workspace/karma/navbar.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/navbar/navbar.component.spec.ts;
    else
        echo "Frontend_should_create_navbar_component FAILED";
        echo "Frontend_should_contain_work_buddy_heading_in_the_navbar_component FAILED";
    fi

    # checking for registration.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/registration" ]
    then
        cp /home/coder/project/workspace/karma/registration.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/registration/registration.component.spec.ts;
    else
        echo "Frontend_should_create_registration_component FAILED";
        echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    fi

    # checking for adminviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/adminviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback/adminviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_adminviewfeedback_component FAILED";
        echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    fi

    # checking for useraddfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/useraddfeedback" ]
    then
        cp /home/coder/project/workspace/karma/useraddfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/useraddfeedback/useraddfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_useraddfeedback_component FAILED";
        echo "Frontend_should_contain_add_feedback_heading_in_the_useraddfeedback_component FAILED";
    fi

    # checking for authguard.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/authguard" ]
    then
        cp /home/coder/project/workspace/karma/authguard.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/authguard/authguard.component.spec.ts;
    else
        echo "Frontend_authguard_should be created FAILED";
    fi

    # checking for myleave.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/myleave" ]
    then
        cp /home/coder/project/workspace/karma/myleave.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/myleave/myleave.component.spec.ts;
    else
        echo "Frontend_should_create_myleave_component FAILED";
        echo "Frontend_should_contain_leave_requests_heading_in_the_myleave_component FAILED";
    fi

    # checking for usernav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/usernav" ]
    then
        cp /home/coder/project/workspace/karma/usernav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/usernav/usernav.component.spec.ts;
    else
        echo "Frontend_should_create_usernav_component FAILED";
    fi

    # checking for userviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/userviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userviewfeedback/userviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_userviewfeedback_component FAILED";
        echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
    fi

    # checking for mywfh.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/mywfh" ]
    then
        cp /home/coder/project/workspace/karma/mywfh.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/mywfh/mywfh.component.spec.ts;
    else
        echo "Frontend_should_create_mywfh_component FAILED";
        echo "Frontend_should_contain_wfh_requests_heading_in_the_mywfh_component FAILED";
    fi

    # checking for addleave.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/addleave" ]
    then
        cp /home/coder/project/workspace/karma/addleave.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/addleave/addleave.component.spec.ts;
    else
        echo "Frontend_should_create_addleave_component FAILED";
    fi

    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/addwfh" ]
    then
        cp /home/coder/project/workspace/karma/addwfh.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/addwfh/addwfh.component.spec.ts;
    else
        echo "Frontend_should_create_addwfh_component FAILED";
    fi

    if [ -d "/home/coder/project/workspace/angularapp/node_modules" ]; 
    then
        cd /home/coder/project/workspace/angularapp/
        npm test;
    else
        cd /home/coder/project/workspace/angularapp/
        yes | npm install
        npm test
    fi 
else   
    echo "Frontend_should_create_usernav_component FAILED";
    echo "Frontend_should_create_userviewfeedback_component FAILED";
    echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
    echo "Frontend_should_create_adminnav_component FAILED";
    echo "Frontend_should_create_viewemployees_component FAILED";
    echo "Frontend_should_contain_view_employees_heading_in_the_viewemployees_component FAILED";
    echo "Frontend_should_create_auth_service FAILED";
    echo "Frontend_should_create_leave_service FAILED";
    echo "Frontend_should_create_viewleave_component FAILED";
    echo "Frontend_should_contain_leave_requests_for_approval_heading_in_the_viewleave_component FAILED";
    echo "Frontend_should_create_error_component FAILED";
    echo "Frontend_should_contain_wrong_message_in_the_error_component FAILED";
    echo "Frontend_should_create_employee_service FAILED";
    echo "Frontend_should_create_wfh_service FAILED";
    echo "Frontend_should_create_home_component FAILED";
    echo "Frontend_should_contain_workbuddy_heading_in_the_home_component FAILED";
    echo "Frontend_should_create_feedback_service FAILED";
    echo "Frontend_should_create_viewwfh_component FAILED";
    echo "Frontend_should_contain_wfh_requests_for_approval_heading_in_the_viewwfh_component FAILED";
    echo "Frontend_should_create_login_component FAILED";
    echo "Frontend_should_contain_login_heading_in_the_login_component FAILED";
    echo "Frontend_should_create_navbar_component FAILED";
    echo "Frontend_should_contain_work_buddy_heading_in_the_navbar_component FAILED";
    echo "Frontend_should_create_registration_component FAILED";
    echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    echo "Frontend_should_create_adminviewfeedback_component FAILED";
    echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    echo "Frontend_should_create_useraddfeedback_component FAILED";
    echo "Frontend_should_contain_add_feedback_heading_in_the_useraddfeedback_component FAILED";
    echo "Frontend_should_create_myleave_component FAILED";
    echo "Frontend_should_contain_leave_requests_heading_in_the_myleave_component FAILED";
    echo "Frontend_should_create_usernav_component FAILED";
    echo "Frontend_should_create_userviewfeedback_component FAILED";
    echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
    echo "Frontend_should_create_mywfh_component FAILED";
    echo "Frontend_should_contain_wfh_requests_heading_in_the_mywfh_component FAILED";
    echo "Frontend_should_create_viewloan_component FAILED";
    echo "Frontend_should_create_addleave_component FAILED";
    echo "Frontend_should_create_addwfh_component FAILED";
    echo "Frontend_authguard_should be created FAILED";

fi
