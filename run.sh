#!/bin/bash
if [ -d "/home/coder/project/workspace/dotnetapp/" ]
then
    echo "project folder present"
    # checking for src folder
    if [ -d "/home/coder/project/workspace/dotnetapp/" ]
    then
        cp -r /home/coder/project/workspace/nunit/test/TestProject /home/coder/project/workspace/;
		cp -r /home/coder/project/workspace/nunit/test/dotnetapp.sln /home/coder/project/workspace/dotnetapp;
    cd /home/coder/project/workspace/TestProject || exit;
     dotnet clean;    
     dotnet build && dotnet test -l "console;verbosity=normal";
    else
	    echo "Backend_Test_Post_Method_Register_Manager_Returns_HttpStatusCode_OK FAILED";
        echo "Backend_Test_Post_Method_Login_Manager_Returns_HttpStatusCode_OK FAILED";
	    echo "Backend_Test_Get_All_LeaveRequest_With_Token_By_Manager_Returns_HttpStatusCode_OK FAILED";
        echo "Backend_Test_Get_All_LeaveRequest_Without_Token_By_Manager_Returns_HttpStatusCode_Unauthorized FAILED";
        echo "Backend_Test_Get_Method_Get_LeaveRequestById_In_Leave_Service_Fetches_LeaveRequest_Successfully FAILED";
        echo "Backend_Test_Put_Method_UpdateLeaveRequest_In_Leave_Service_Updates_LeaveRequest_Successfully FAILED";
        echo "Backend_Test_Delete_Method_DeleteWfhRequest_In_Wfh_Service_Deletes_WfhRequest_Successfully FAILED";
        echo "Backend_Test_Get_Method_GetWfhRequestsByUserId_In_Wfh_Service_Fetches_Successfully FAILED";
	    echo "Backend_Test_Get_Method_GetAllWfhRequests_In_WfhService_Returns_All_WfhRequests FAILED";
        echo "Backend_Test_Get_Method_GetAllLeaveRequests_In_LeaveService_Returns_All_LeaveRequests FAILED";
		echo "Backend_Test_Post_Method_AddWfhRequest_In_WfhService_Occurs_WfhException_For_Overlapping_Request FAILED";
        echo "Backend_Test_Post_Method_AddFeedback_In_Feedback_Service_Posts_Successfully FAILED";
        echo "Backend_Test_Delete_Method_Feedback_In_Feeback_Service_Deletes_Successfully FAILED";
	    echo "Backend_Test_Get_Method_GetFeedbacksByUserId_In_Feedback_Service_Fetches_Successfully FAILED";		
    fi
else  
	    echo "Backend_Test_Post_Method_Register_Manager_Returns_HttpStatusCode_OK FAILED";
        echo "Backend_Test_Post_Method_Login_Manager_Returns_HttpStatusCode_OK FAILED";
	    echo "Backend_Test_Get_All_LeaveRequest_With_Token_By_Manager_Returns_HttpStatusCode_OK FAILED";
        echo "Backend_Test_Get_All_LeaveRequest_Without_Token_By_Manager_Returns_HttpStatusCode_Unauthorized FAILED";
        echo "Backend_Test_Get_Method_Get_LeaveRequestById_In_Leave_Service_Fetches_LeaveRequest_Successfully FAILED";
        echo "Backend_Test_Put_Method_UpdateLeaveRequest_In_Leave_Service_Updates_LeaveRequest_Successfully FAILED";
        echo "Backend_Test_Delete_Method_DeleteWfhRequest_In_Wfh_Service_Deletes_WfhRequest_Successfully FAILED";
        echo "Backend_Test_Get_Method_GetWfhRequestsByUserId_In_Wfh_Service_Fetches_Successfully FAILED";
	    echo "Backend_Test_Get_Method_GetAllWfhRequests_In_WfhService_Returns_All_WfhRequests FAILED";
        echo "Backend_Test_Get_Method_GetAllLeaveRequests_In_LeaveService_Returns_All_LeaveRequests FAILED";
		echo "Backend_Test_Post_Method_AddWfhRequest_In_WfhService_Occurs_WfhException_For_Overlapping_Request FAILED";
        echo "Backend_Test_Post_Method_AddFeedback_In_Feedback_Service_Posts_Successfully FAILED";
        echo "Backend_Test_Delete_Method_Feedback_In_Feeback_Service_Deletes_Successfully FAILED";
	    echo "Backend_Test_Get_Method_GetFeedbacksByUserId_In_Feedback_Service_Fetches_Successfully FAILED";
        fi