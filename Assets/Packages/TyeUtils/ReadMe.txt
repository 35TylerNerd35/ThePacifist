using TyeUtils;

#TweenUtils

    Initialise Instance of tweenUtils:
        TweenUtils tweenUtils = new();

    Add Callback Method (Optional):
        tweenUtils.TweenFinished += MyCallbackMethod;

    Start Tween:
        tweenUtils.StartVector3Tween(this, [<Vector3> startVector], [<Vector3> endVector], [VectorUpdateMethod], [-OPTIONAL-<float> duration], [-OPTIONAL-<AnimationCurve> curve]);
        tweenUtils.StartFloatTween(this, [<float> startFloat], [<float> endFloat], [floatUpdateMethod], [-OPTIONAL-<float> duration], [-OPTIONAL-<AnimationCurve> curve]);
        tweenUtils.StartPositionTween(this, [<float> startFloat], [<float> endFloat], [floatUpdateMethod], [<float> diameter] [-OPTIONAL-<float> duration], [-OPTIONAL-<AnimationCurve> curve]);

    UpdateMethods:
        You could either create a Function to handle it:
            public void UpdateVector(Vector3 val)
            {
                //Do something with val
            }
            public void UpdateFloat(float val)
            {
                //Do something with val
            }
        or you could create a lambda expression in place of the square brackets:
            , val => {//Do something with val},

    Functions:
        tweenUtils.StopTween();

    Vars (Optional):
        tweenUtils.IsTweening           [bool]
        tweenUtils.TweenProgress        [float <0-1>]


    -
#GameLogs

    Commands:
        GameLogs.Log();
        GameLogs.Warning();
        GameLogs.Error();

    Paramaters (Required):
        Title (string): The 'header' of the log
        Message (string): The content of the log

    Paramaters (Optional):
        priority (int 0-3): the priority of the content (blue, orange, red), defaults to 0

    Examples:
        GameLogs.Log("HEADER", "I am a script");
        GameLogs.Warning("I am warning", "I am to warn of something", 2);
        GameLogs.Error("ERROR!!", "I have messed up", 3);