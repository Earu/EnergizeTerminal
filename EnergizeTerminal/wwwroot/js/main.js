let input = $(".terminal-input");
let token = "NOT_ASSIGNED";
let commandcallbacks = {
    "login": x => {
        if (x.success)
        {
            token = x.result;
            Prompt("Successfully logged in!");
        }
        else
            Prompt(x.result);
    },
    "logout": x => {
        token = "NOT_ASSIGNED";
        Prompt(x.result);
    }
};

input.focus();

$(".container").on("click",(_) => input.focus());

input.on("keyup",(_) => $(".new-output").text(input.val()));

function Prompt(msg) {
    msg = typeof msg !== "string" ? toString(msg) : msg;
    $(".terminal").append(`<p class='prompt-simple'>${msg}</p><p class='prompt output new-output'></p>`);
    window.scrollTo(0, document.body.scrollHeight);
}

function ProcessString(str) {
    let parts = str.split(" ");
    let cmd = parts[0];
    parts.splice(0, 1);

    return { command: cmd, parameters: parts, token: token };
}

function OnCommandSuccess(cmd, cmdres) {
    if (commandcallbacks[cmd])
        commandcallbacks[cmd](cmdres);
    else
        Prompt(cmdres.result);
}

function HandleCommand(str) {
    if (str.trim() === "") return;

    let input = $(".terminal-input");

    $(".new-output").removeClass("new-output");
    input.val("");

    let cmdobject = ProcessString(str);

    $.ajax({
        contentType: "application/json",
        data: JSON.stringify(cmdobject),
        dataType: "json",
        success: (data) => OnCommandSuccess(cmdobject.command, data),
        error: (e) => Prompt(`${e.statusCode}${e.statusText}`),
        type: "POST",
        url: "/commands"
    });
}

$(".terminal-form").on("submit",function(e)
{
    e.preventDefault();
    let str = $(this).children($(".terminal-input")).val().toLowerCase();

    HandleCommand(str);
});