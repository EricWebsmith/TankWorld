x = load("-ascii","x_train.txt");
y = load("-ascii","y_train.txt");

input_layer_size  = 40;  % 40 Input Images of Digits
hidden_layer_size = 15;   % 15 hidden units
num_labels = 6;          % 6 labels, from 1 to 10 

options = optimset('MaxIter', 100);

%theta1 41*15=615
%theta2 16*6=96
nn_params=[1:711];

initial_Theta1 = randInitializeWeights(input_layer_size, hidden_layer_size);
initial_Theta2 = randInitializeWeights(hidden_layer_size, num_labels);

% Unroll parameters
initial_nn_params = [initial_Theta1(:) ; initial_Theta2(:)];

lambda = 0;


costFunction = @(p) Tank_nnCostFunction(p, ...
                                   input_layer_size, ...
                                   hidden_layer_size, ...
                                   num_labels, x, y, lambda);
                                   
                                   
% Now, costFunction is a function that takes in only one argument (the
% neural network parameters)
[nn_params, cost] = fmincg(costFunction, initial_nn_params, options);

% Obtain Theta1 and Theta2 back from nn_params
Theta1 = reshape(nn_params(1:hidden_layer_size * (input_layer_size + 1)), ...
                 hidden_layer_size, (input_layer_size + 1));

Theta2 = reshape(nn_params((1 + (hidden_layer_size * (input_layer_size + 1))):end), ...
                 num_labels, (hidden_layer_size + 1));
                                   
                                   
                                   
pred = Tank_predict(Theta1, Theta2, x);
pred
fprintf('\nTraining Set Accuracy: %f\n', mean(double(pred == y)) * 100);